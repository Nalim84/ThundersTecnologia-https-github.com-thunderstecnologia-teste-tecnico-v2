using Thunders.TechTest.ApiService;
using Thunders.TechTest.IoC;
using Thunders.TechTest.OutOfBox.Contexts;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.EnableAnnotations();
});
builder.Services.AddHttpClient();


builder.Services.AddScoped<IMessageSender, RebusMessageSender>();

var features = Features.BindFromConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder());
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<DefaultContext>(builder.Configuration);
}

//Extensions.AddServiceDefaults(builder);
DependencyResolver.RegisterWebApplicationDependencies(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.MapControllers();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();



app.Run();
