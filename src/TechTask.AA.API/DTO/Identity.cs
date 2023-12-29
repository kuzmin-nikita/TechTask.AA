namespace TechTask.AA.API.DTO
{
    /// <summary>
    /// DTO for authorization
    /// </summary>
    /// <param name="Username">Username</param>
    /// <param name="Password">Password</param>
    public sealed record AuthorizeDto(
        string Username,
        string Password);

    /// <summary>
    /// DTO with resulting token
    /// </summary>
    /// <param name="Token">JWT</param>
    public sealed record IdentityDto(
        string Token);
}
