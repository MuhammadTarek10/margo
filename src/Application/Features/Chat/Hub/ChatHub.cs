using Microsoft.AspNetCore.SignalR;

namespace Application.Features;

public class ChatHub : Hub<IChatSender>;