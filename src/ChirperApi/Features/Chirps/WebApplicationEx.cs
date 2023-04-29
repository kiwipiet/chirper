using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chirper.Api.Features.Chirps;

public static class WebApplicationEx
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