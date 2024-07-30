using System.Runtime.CompilerServices;

namespace ImageVault.ApiKeyService.Data.Dtos;

public record ApiKeyDto
(
    string UserId,
    string KeyName, 
    string Key, 
    uint KeyCapactiy 
);