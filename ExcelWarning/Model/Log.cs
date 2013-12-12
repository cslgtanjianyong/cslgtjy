using System;
namespace syntec.Model
{
    /// <summary>
    /// Log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Log
    {
        public Log()
        { }
        #region Model
        private int _id;
        private int _ruleid;
        private string _logdescribe;
        private DateTime _logstarttime;
        private int? _stateid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RuleID
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string logDescribe
        {
            set { _logdescribe = value; }
            get { return _logdescribe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime logStartTime
        {
            set { _logstarttime = value; }
            get { return _logstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StateID
        {
            set { _stateid = value; }
            get { return _stateid; }
        }
        #endregion Model

    }
}

