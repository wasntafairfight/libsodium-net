﻿using System.Text;
using Sodium;
using NUnit.Framework;
using System.Diagnostics;

namespace Tests
{
  /// <summary>Tests for the StreamEncryption class</summary>
  [TestFixture]
  public class StreamEncryptionTest
  {
    /// <summary>Verify that the length of the returned key is correct.</summary>
    [Test]
    public void TestGenerateKey()
    {
      Assert.AreEqual(32, StreamEncryption.GenerateKey().Length);
    }

    /// <summary>Verify that the length of the returned key is correct.</summary>
    [Test]
    public void TestGenerateNonce()
    {
      Assert.AreEqual(24, StreamEncryption.GenerateNonce().Length);
    }

    /// <summary>Verify that the length of the returned key is correct.</summary>
    [Test]
    public void TestGenerateNonceChaCha20()
    {
        Assert.AreEqual(8, StreamEncryption.GenerateNonceChaCha20().Length);
    }

    /// <summary>Does StreamEncryption.Encrypt() return the expected value?</summary>
    [Test]
    public void CreateSecretBox()
    {
      var expected = Utilities.HexToBinary("c7b7f04c00e14b02dd56c78c");
      var actual = StreamEncryption.Encrypt(
        Encoding.UTF8.GetBytes("Adam Caudill"),
        Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWX"),
        Encoding.UTF8.GetBytes("12345678901234567890123456789012"));
      Assert.AreEqual(expected, actual);
    }

    /// <summary>Does StreamEncryption.Decrypt() return the expected value?</summary>
    [Test]
    public void OpenSecretBox()
    {
      const string EXPECTED = "Adam Caudill";
      var actual = Encoding.UTF8.GetString(StreamEncryption.Decrypt(
        Utilities.HexToBinary("c7b7f04c00e14b02dd56c78c"),
        Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWX"),
        Encoding.UTF8.GetBytes("12345678901234567890123456789012")));
      Assert.AreEqual(EXPECTED, actual);
    }

    /// <summary>Does StreamEncryption.EncryptChaCha20() return the expected value?</summary>
    [Test]
    public void CreateSecretBoxChaCha20()
    {
        var expected = Utilities.HexToBinary("a6ce598d8b865fb328581bcd");
        var actual = StreamEncryption.EncryptChaCha20(
          Encoding.UTF8.GetBytes("Adam Caudill"),
          Encoding.UTF8.GetBytes("ABCDEFGH"),
          Encoding.UTF8.GetBytes("12345678901234567890123456789012"));
        Assert.AreEqual(expected, actual);
    }

    /// <summary>Does StreamEncryption.DecryptChaCha20() return the expected value?</summary>
    [Test]
    public void OpenSecretBoxChaCha20()
    {
        const string EXPECTED = "Adam Caudill";
        var actual = Encoding.UTF8.GetString(StreamEncryption.DecryptChaCha20(
          Utilities.HexToBinary("a6ce598d8b865fb328581bcd"),
          Encoding.UTF8.GetBytes("ABCDEFGH"),
          Encoding.UTF8.GetBytes("12345678901234567890123456789012")));
        Assert.AreEqual(EXPECTED, actual);
    }
  }
}
