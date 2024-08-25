using ImageVault.UserService.Data.Dtos;

namespace ImageVault.UserService.Data.Interfaces;

/// <summary>
/// Interface representing the user repository for managing user data.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="userData">The data of the user to be added.</param>
    /// <param name="id">The unique identifier for the user.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="OperationResult{T}"/>
    /// </returns>
    Task<OperationResult<UserData>> AddUser(UserData userData, string id);

    /// <summary>
    /// Retrieves a user from the database by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to be retrieved.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="OperationResult{T}"/>
    /// </returns>
    Task<OperationResult<UserData>> GetUser(string email);

    /// <summary>
    /// Updates the data of an existing user in the database.
    /// </summary>
    /// <param name="newUserData">The new data for the user.</param>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="OperationResult{T}"/>
    /// </returns>
    Task<OperationResult<UserData>> UpdateUser(UpdateUser newUserData, string id);

    /// <summary>
    /// Deletes a user from the database by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to be deleted.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a boolean value indicating 
    /// whether the deletion was successful.
    /// </returns>
    Task<bool> DeleteUser(string email);
}
