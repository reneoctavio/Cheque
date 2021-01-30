// -------------------------------------------------------------------------
// Cheque - An Application to help you keep track of checks in your custody
// Copyright (C) 2013 Rene Octavio Queiroz Dias
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Source code at <https://github.com/reneoctavio/Cheque>.
// -------------------------------------------------------------------------
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

