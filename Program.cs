﻿using System;

namespace com.github.xiangyuecn.rsacsharp {
	/// <summary>
	/// RSA、RSA_PEM测试控制台主程序
	/// GitHub: https://github.com/xiangyuecn/RSA-csharp
	/// </summary>
	class Program {
		static void RSATest() {
			var rsa = new RSA(512);
			Console.WriteLine("【" + rsa.KeySize + "私钥（XML）】：");
			Console.WriteLine(rsa.ToXML());
			Console.WriteLine();
			Console.WriteLine("【" + rsa.KeySize + "私钥（PEM）】：");
			Console.WriteLine(rsa.ToPEM_PKCS1());
			Console.WriteLine();
			Console.WriteLine("【" + rsa.KeySize + "公钥（PEM）】：");
			Console.WriteLine(rsa.ToPEM_PKCS1(true));
			Console.WriteLine();

			var str = "abc内容123";
			var en = rsa.Encode(str);
			Console.WriteLine("【加密】：");
			Console.WriteLine(en);

			Console.WriteLine("【解密】：");
			Console.WriteLine(rsa.DecodeOrNull(en));

			Console.WriteLine("【签名SHA1】：");
			Console.WriteLine(rsa.Sign("SHA1", str));
			Console.WriteLine();

			var rsa2 = new RSA(rsa.ToPEM_PKCS8(), true);
			Console.WriteLine("【用PEM新创建的RSA是否和上面的一致】：");
			Console.WriteLine("XML：" + (rsa2.ToXML() == rsa.ToXML()));
			Console.WriteLine("PKCS1：" + (rsa2.ToPEM_PKCS1() == rsa.ToPEM_PKCS1()));
			Console.WriteLine("PKCS8：" + (rsa2.ToPEM_PKCS8() == rsa.ToPEM_PKCS8()));

			var rsa3 = new RSA(rsa.ToXML());
			Console.WriteLine("【用XML新创建的RSA是否和上面的一致】：");
			Console.WriteLine("XML：" + (rsa3.ToXML() == rsa.ToXML()));
			Console.WriteLine("PKCS1：" + (rsa3.ToPEM_PKCS1() == rsa.ToPEM_PKCS1()));
			Console.WriteLine("PKCS8：" + (rsa3.ToPEM_PKCS8() == rsa.ToPEM_PKCS8()));

			//--------RSA_PEM验证---------
			RSA_PEM pem = rsa.ToPEM();
			Console.WriteLine("【RSA_PEM是否和原始RSA一致】：");
			Console.WriteLine(pem.KeySize + "位");
			Console.WriteLine("XML：" + (pem.ToXML(false) == rsa.ToXML()));
			Console.WriteLine("PKCS1：" + (pem.ToPEM(false, false) == rsa.ToPEM_PKCS1()));
			Console.WriteLine("PKCS8：" + (pem.ToPEM(false, true) == rsa.ToPEM_PKCS8()));
			Console.WriteLine("仅公钥：");
			Console.WriteLine("XML：" + (pem.ToXML(true) == rsa.ToXML(true)));
			Console.WriteLine("PKCS1：" + (pem.ToPEM(true, false) == rsa.ToPEM_PKCS1(true)));
			Console.WriteLine("PKCS8：" + (pem.ToPEM(true, true) == rsa.ToPEM_PKCS8(true)));

			var rsa4 = new RSA(new RSA_PEM(pem.Key_Modulus, pem.Key_Exponent, pem.Key_D));
			Console.WriteLine("【用n、e、d构造解密】");
			Console.WriteLine(rsa4.DecodeOrNull(en));
		}




		static void Main(string[] args) {
			Console.WriteLine("---------------------------------------------------------");
			Console.WriteLine("◆◆◆◆◆◆◆◆◆◆◆◆ RSA测试 ◆◆◆◆◆◆◆◆◆◆◆◆");
			Console.WriteLine("---------------------------------------------------------");

			RSATest();

			Console.WriteLine("-------------------------------------------------------------");
			Console.WriteLine("◆◆◆◆◆◆◆◆◆◆◆◆ 回车退出... ◆◆◆◆◆◆◆◆◆◆◆◆");
			Console.WriteLine();
			Console.ReadLine();
		}
	}
}
