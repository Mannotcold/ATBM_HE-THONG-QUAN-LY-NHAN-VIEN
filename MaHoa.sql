SELECT * FROM all_objects WHERE object_name = 'DBMS_CRYPTO';
CREATE OR REPLACE FUNCTION encrypt_data(p_string IN VARCHAR2, p_key IN VARCHAR2) RETURN RAW AS
    encrypted_data RAW(2000);
    r_key RAW(2000);
BEGIN
    -- Convert the key to RAW format
    r_key := UTL_RAW.CAST_TO_RAW(p_key);

    -- Encrypt the string using the key
    encrypted_data := DBMS_CRYPTO.ENCRYPT(
        src => UTL_I18N.STRING_TO_RAW(p_string, 'AL32UTF8'),
        typ => DBMS_CRYPTO.DES_CBC_PKCS5,
        key => r_key
    );
    
    -- Return the encrypted data
    RETURN encrypted_data;
END;
CREATE OR REPLACE FUNCTION DE_CRYPT (data_encrypt_raw raw, p_key VARCHAR2)
RETURN NVARCHAR2
AS
    r_key RAW(200) := UTL_RAW.CAST_TO_RAW (convert (p_key, 'AL32UTF8', 'US7ASCII'));
    r_decrypt RAW(500);
    decrypted_data VARCHAR2(100);
BEGIN
    r_decrypt := dbms_crypto.Decrypt(src => data_encrypt_raw, 
                                        typ => DBMS_CRYPTO.DES_CBC_PKCS5,
                                        key => r_key);
   
    decrypted_data := UTL_RAW.CAST_TO_NVARCHAR2(r_decrypt);
    
    
    RETURN decrypted_data;
END;

SELECT encrypt_data('Hello, world!', 'my_secret_key') FROM dual;
DECLARE
  v_dummy NUMBER;
BEGIN
  v_dummy := DBMS_CRYPTO.DES_CBC_PKCS5;
END;

SELECT DBMS_CRYPTO.ENCRYPT(DUMMY, DBMS_CRYPTO.AES_CBC_PKCS5, UTL_RAW.CAST_TO_RAW('my_secret_key')) FROM DUAL;
SELECT DE_CRYPT('AE4E2B0D63C4F6A2CFE0A21ECCB3AA1B','my_secret_key') FROM dual;