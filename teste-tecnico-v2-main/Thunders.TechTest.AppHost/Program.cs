using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var rabbitMqPassword = builder.AddParameter("RabbitMqPassword", true);
var rabbitMq = builder.AddRabbitMQ("RabbitMq", password: rabbitMqPassword)
    .WithDataVolume()
    .WithVolume("/etc/rabbitmq")
    .WithManagementPlugin();

var sqlServerPassword = 
    builder.AddParameter("SqlServerInstancePassword", true);
var sqlServer = 
    builder.AddSqlServer("SqlServerInstance", sqlServerPassword, port: 1433 )
    .WithDataVolume();

var database = 
    sqlServer.AddDatabase("ThundersTechTestDb", "ThundersTechTest");

var apiService = builder.AddProject<Projects.Thunders_TechTest_ApiService>("thunders-techtest-apiservice") //apiservice
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.Thunders_TechTest_Migration>("thunders-techtest-migration").WithReference(database).WaitFor(database);


builder.AddProject<Projects.Thunders_TechTest_Worker>("thunders-techtest-worker")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();