using System;
namespace saivian.Model
{
	/// <summary>
	/// user_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class user_info
	{
		public user_info()
		{} 
		#region Model
		private int _u_id;
		private string _u_name;
		private int? _s_id;
		private string _s_name;
		private string _s_pwd;
		private string _email;
		private string _address;
		private string _s_create_time;
		private string _zip;
		private string _phone;
		private string _city;
		private string _create_time;
		private string _create_user;
		private int? _a_level;
		/// <summary>
		/// 
		/// </summary>
		public int u_id
		{
			set{ _u_id=value;}
			get{return _u_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_name
		{
			set{ _u_name=value;}
			get{return _u_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? s_id
		{
			set{ _s_id=value;}
			get{return _s_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s_name
		{
			set{ _s_name=value;}
			get{return _s_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s_pwd
		{
			set{ _s_pwd=value;}
			get{return _s_pwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s_create_time
		{
			set{ _s_create_time=value;}
			get{return _s_create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zip
		{
			set{ _zip=value;}
			get{return _zip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string city
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string create_time
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string create_user
		{
			set{ _create_user=value;}
			get{return _create_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? a_level
		{
			set{ _a_level=value;}
			get{return _a_level;}
		}
		#endregion Model



	}
}

