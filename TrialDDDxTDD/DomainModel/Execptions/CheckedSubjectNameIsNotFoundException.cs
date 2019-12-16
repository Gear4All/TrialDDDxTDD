using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class CheckedSubjectNameIsNotFoundException : Exception
    {
        public CheckedSubjectNameIsNotFoundException()
       : base()
        {
        }

        public CheckedSubjectNameIsNotFoundException(string message)
            : base(message)
        {
        }

        public CheckedSubjectNameIsNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        //逆シリアル化コンストラクタ。このクラスの逆シリアル化のために必須。
        //アクセス修飾子をpublicにしないこと！（詳細は後述）
        protected CheckedSubjectNameIsNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
