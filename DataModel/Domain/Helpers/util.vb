Imports System.Security.Cryptography
Imports System.Text

Public Class util



    Shared Function ReturnEncodedPassword(password As String) As String

        Dim salt As String = "portal4cpi"
        Dim bytHashedData As Byte()
        Dim encoder As New UTF8Encoding()
        Dim md5Hasher As New MD5CryptoServiceProvider

        ' Get Bytes for "password"
        Dim passwordBytes As Byte() = encoder.GetBytes(password)

        ' Get Bytes for "salt"
        Dim saltBytes As Byte() = encoder.GetBytes(salt)

        ' Creat new Array to store both "password" and "salt" bytes
        Dim passwordAndSaltBytes As Byte() =
        New Byte(passwordBytes.Length + saltBytes.Length - 1) {}

        ' Store "password" bytes
        For i As Integer = 0 To passwordBytes.Length - 1
            passwordAndSaltBytes(i) = passwordBytes(i)
        Next

        ' Append "salt" bytes
        For i As Integer = 0 To saltBytes.Length - 1
            passwordAndSaltBytes(i + passwordBytes.Length) = saltBytes(i)
        Next

        ' Compute hash value for "password" and "salt" bytes
        bytHashedData = md5Hasher.ComputeHash(passwordAndSaltBytes)

        ' Convert result into a base64-encoded string.
        Dim hashValue As String
        hashValue = Convert.ToBase64String(bytHashedData)

        Return hashValue

    End Function
End Class
