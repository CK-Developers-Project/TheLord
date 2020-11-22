using UnityEngine;
using UnityEditor;

namespace Developers.Structure
{
    public enum ReturnCode
    {
        Success = 0,
        Failed = -1
    }

    public enum OperationCode : byte
    {
        Login,              // 로그인
        UserResistration,   // 유저 닉네임 및 종족 선택
    }

    public enum EventCode : byte
    {

    }


    public class GamePacket
    {

    }



    public interface IGoldTransfer
    {
        // FIXME : 현재는 서버를 사용하지 않으므로 클라로 대체
        int Cost { get; }
    }

    public struct AdditionGold : IGoldTransfer
    {
        int cost;

        public AdditionGold ( int cost ) => this.cost = cost;

        public int Cost { get => cost; }
    }

}