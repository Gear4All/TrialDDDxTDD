///参考：https://hackers-high.com/c-sharp/develop-self-exception/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class NotBalanceBetweenDebitAndCreditException : Exception
    {
        public NotBalanceBetweenDebitAndCreditException()
       : base()
        {
        }

        public NotBalanceBetweenDebitAndCreditException(string message)
            : base(message)
        {
        }

        public NotBalanceBetweenDebitAndCreditException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        //逆シリアル化コンストラクタ。このクラスの逆シリアル化のために必須。
        //アクセス修飾子をpublicにしないこと！（詳細は後述）
        protected NotBalanceBetweenDebitAndCreditException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
