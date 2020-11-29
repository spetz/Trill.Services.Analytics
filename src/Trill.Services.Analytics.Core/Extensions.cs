using System.Linq;
using System.Text;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.Prometheus;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Tracing.Jaeger;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Trill.Services.Analytics.Core.Decorators;
using Trill.Services.Analytics.Core.Events.External;
using Trill.Services.Analytics.Core.Logging;
using Trill.Services.Analytics.Core.Mongo;
using Trill.Services.Analytics.Core.Services;

namespace Trill.Services.Analytics.Core
{
    public static class Extensions
    {
        public static IConveyBuilder AddCore(this IConveyBuilder builder)
        {
            builder.Services
                .AddScoped<LogContextMiddleware>()
                .AddScoped<IMessageBroker, MessageBroker>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IStoriesService, StoriesService>()
                .AddScoped<ITagsService, TagsService>()
                .AddScoped<ITrendingService, TrendingService>()
                .AddScoped<IUsersService, UsersService>()
                .AddScoped<IDatabaseProvider, DatabaseProvider>();

            builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddJwt()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddMongo()
                .AddRabbitMq()
                .AddMessageOutbox(o => o.AddMongo())
                .AddRedis()
                .AddPrometheus()
                .AddJaeger()
                .AddWebApiSwaggerDocs();

            builder.Services.AddSingleton<ICorrelationIdFactory, CorrelationIdFactory>();
            
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(LoggingEventHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));
            
            return builder;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogContextMiddleware>()
                .UseJaeger()
                .UsePrometheus()
                .UseSwaggerDocs()
                .UseConvey()
                .UseAccessTokenValidator()
                .UseMongo()
                .UsePublicContracts<ContractAttribute>()
                .UseAuthentication()
                .UseRabbitMq()
                .SubscribeEvent<UserCreated>()
                .SubscribeEvent<UserFollowed>()
                .SubscribeEvent<UserUnfollowed>();

            return app;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                : null;
        
        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
    }
}