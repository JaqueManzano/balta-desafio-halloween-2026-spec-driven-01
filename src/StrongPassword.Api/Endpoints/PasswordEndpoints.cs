using StrongPassword.Api.Services;

namespace StrongPassword.Api.Endpoints;

public static class PasswordEndpoints
{
    public static IEndpointRouteBuilder MapPasswordEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1/passwords");

        group.MapPost("", async (IPasswordService service, CancellationToken cancellationToken) =>
        {
            var password = await service.CreateAsync(cancellationToken);
            var response = new PasswordResponse(password.Id, password.Password);
            return Results.CreatedAtRoute("GetPasswordById", new { guid = password.Id }, response);
        });

        group.MapGet("/{guid}", async (
            string guid,
            IPasswordQueryService service,
            CancellationToken cancellationToken) =>
        {
            if (!Guid.TryParse(guid, out var id))
            {
                return Results.Problem(
                    title: "GUID inválido",
                    detail: "O GUID informado não está em um formato válido.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var password = await service.GetByIdAsync(id, cancellationToken);
            if (password is null)
            {
                return Results.Problem(
                    title: "Senha não encontrada",
                    detail: "Não foi encontrada uma senha para o GUID informado.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            return Results.Ok(new PasswordResponse(password.Id, password.Password));
        }).WithName("GetPasswordById");

        return endpoints;
    }
}
