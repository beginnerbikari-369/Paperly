using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class DocumentHub : Hub
{
    public async Task JoinDocument(string documentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, documentId);
    }

    public async Task LeaveDocument(string documentId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, documentId);
    }

    public async Task SendDocumentUpdate(string documentId, string content)
    {
        await Clients.OthersInGroup(documentId).SendAsync("ReceiveDocumentUpdate", content);
    }
}
