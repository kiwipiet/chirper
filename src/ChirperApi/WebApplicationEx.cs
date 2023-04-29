using System.Diagnostics;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chirper.Api;

public static partial class WebApplicationEx
{
    public static void MapChirps(this WebApplication app)
    {
        app.MapPost("/chirps", async (IMediator mediator, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)]Chirp chirp, CancellationToken cancellationToken) =>
            {
                if (chirp == null) throw new ArgumentNullException(nameof(chirp));
                await mediator.Send(new PostChirp(chirp), cancellationToken);
                return Results.Accepted();
            })
            .WithName("PostChirp")
            .WithOpenApi(); ;
    }
}

public sealed record Chirp(string Text);

public sealed record PostChirp(Chirp Chirp) : ICommand;

public sealed class PostChirpHandler : ICommandHandler<PostChirp>
{
    public async ValueTask<Unit> Handle(PostChirp request, CancellationToken cancellationToken)
    {
        Trace.WriteLine($"Handling Chirp: '{request.Chirp.Text}'");
        await Task.Delay(TimeSpan.FromSeconds(5));
        Trace.WriteLine(cancellationToken.IsCancellationRequested
            ? $"Chirp Cancelled: '{request.Chirp.Text}'"
            : $"Handled Chirp: '{request.Chirp.Text}'");
        return Unit.Value;
    }
}