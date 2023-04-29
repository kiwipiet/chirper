using Mediator;
using System.Diagnostics;

namespace Chirper.Api.Features.Chirps;

public sealed record PostChirp(Chirp Chirp) : ICommand;

public sealed class PostChirpHandler : ICommandHandler<PostChirp>
{
    public async ValueTask<Unit> Handle(PostChirp request, CancellationToken cancellationToken)
    {
        Trace.WriteLine($"Handling Chirp: '{request.Chirp.Text}'");

        Trace.WriteLine(cancellationToken.IsCancellationRequested
            ? $"Chirp Cancelled: '{request.Chirp.Text}'"
            : $"Handled Chirp: '{request.Chirp.Text}'");
        return Unit.Value;
    }
}