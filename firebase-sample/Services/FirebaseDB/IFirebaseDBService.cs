using System;
namespace firebasesample.Services.FirebaseDB
{
    public interface IFirebaseDBService
    {
        void Connect();
        void GetMessage();
        void SetMessage(String message);
        String GetMessageKey();
    }
}
