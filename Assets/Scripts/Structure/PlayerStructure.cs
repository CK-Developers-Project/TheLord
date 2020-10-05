namespace Developers.Structure
{
    
    public class PlayerInfo
    {

        public int Gold { get; set; }

    }


    public interface IGoldTransfer
    {
        // FIXME : 현재는 서버를 사용하지 않으므로 클라로 대체
        int Cost { get; }
    }   
}