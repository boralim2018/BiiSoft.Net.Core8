﻿using System.Threading.Tasks;

namespace BiiSoft.Authorization.Users
{
    public interface IUserEmailer
    {
        /// <summary>
        /// Send email activation link to user's email address.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email activation link</param>
        /// <param name="plainPassword">
        /// Can be set to user's plain password to include it in the email.
        /// </param>
        Task SendEmailActivationLinkAsync(string appName, User user, string link, string plainPassword = null);
        
        /// <summary>
        /// Sends a password reset link to user's email.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Password reset link (optional)</param>
        Task SendPasswordResetLinkAsync(string appName, User user, string link = null);
        Task SendPasswordResetLinkAsyncNew(string appName, User user, string link = null);
        /// <summary>
        /// Sends an email for unread chat message to user's email.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senderUsername"></param>
        /// <param name="senderTenancyName"></param>
        /// <param name="chatMessage"></param>
        //void TryToSendChatMessageMail(User user, string senderUsername, string senderTenancyName, ChatMessage chatMessage);

        Task SetEmail(string emailAddress, string message, string title);
    }
}
