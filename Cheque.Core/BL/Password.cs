using System;

namespace Cheque.BL.Password
{
	public class Password : Cheque.BL.Contracts.BusinessEntityBase
	{
		public string SecurePwd { get; private set; }

		[Cheque.DL.SQLite.Ignore]
		public Crypto Crypto{ get; private set; }

		public Password ()
		{
			Crypto = new Crypto (Crypto.CryptoTypes.encTypeTripleDES);
		}

		public Password (string text)
		{
			Crypto = new Crypto (Crypto.CryptoTypes.encTypeTripleDES);
			SecurePwd = Crypto.Encrypt (text);
		}

		public void SetPassword (string passwd)
		{
			SecurePwd = Crypto.Encrypt (passwd);
		}

		public string DecriptedPasswd ()
		{
			return Crypto.Decrypt (SecurePwd);
		}
	}
}
//public class testCrypt
//{
//    public void testEncryption()
//    {
//        string input = "Thi$ is @ str!&n to tEst encrypti0n!";
//        Crypto c = new Crypto(Utils.Crypto.CryptoTypes.encTypeTripleDES);
//        string s1 = c.Encrypt(input);
//        string s2 = c.Decrypt(s1);
//        Assert.IsTrue(s2 == input);
//        s1 = Hashing.Hash(input);
//        s2 = Hashing.Hash(input,Utils.Hashing.HashingTypes.MD5);
//        Assert.IsTrue(s1 == s2);
//        Assert.IsTrue( Hashing.isHashEqual(input,s1));
//        s1 = Hashing.Hash(input,Utils.Hashing.HashingTypes.SHA512);
//    }
//}

