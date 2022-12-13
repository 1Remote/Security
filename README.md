# 1Remote.Security

Provides a simple string encryption solution for 1Remote.

The encryption security is low. It is recommended for strings that are not allowed to be saved in plain text.

## Algorithms

- SimpleStringEncipher

  The algorithm is based on AES and random password(random string + salt)


# How to use


1. Set your salt
```
_1Remote.Security.Config.SetSalt("abc");
```

2. Encrypt
```
var encrypted = _1Remote.Security.Encryption.SimpleStringEncipher.Encrypt("Hello World");
```

3. Decrypt
```
var decrypted = _1Remote.Security.Encryption.SimpleStringEncipher.Decrypt(encrypted);
```