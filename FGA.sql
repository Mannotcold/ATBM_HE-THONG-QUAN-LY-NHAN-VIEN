--4a
BEGIN
    DBMS_FGA.DROP_POLICY(
        object_schema   =>  'quanly',
        object_name     =>  'PHANCONG',
        policy_name     =>  'AUDIT_UPDATE_THOIGIAN_PHANCONG');
END;
/

BEGIN
  DBMS_FGA.ADD_POLICY(
   object_schema      => 'quanly',
   object_name        => 'PHANCONG',
   policy_name        => 'AUDIT_UPDATE_THOIGIAN_PHANCONG',
   enable             =>  TRUE,
   statement_types    => 'UPDATE',
   audit_column       => 'THOIGIAN',
   audit_trail        =>  DBMS_FGA.DB + DBMS_FGA.EXTENDED);
END;
/
BEGIN
    DBMS_FGA.ADD_POLICY(
        object_schema   =>  'QUANLY',
        object_name     =>  'NHANVIEN_V',
        policy_name     =>  'CHECK_LUONG_PHUCAP_ON_NHANVIEN',
        enable          =>   TRUE,
        statement_types =>  'SELECT',
        audit_column    =>  'LUONG, PHUCAP',
        audit_condition => 'MANV != SYS_CONTEXT(''USERENV'',''SESSION_USER'')');
END;
--4C

BEGIN
    dbms_fga.ADD_POLICY (
        OBJECT_SCHEMA => 'QUANLY',
        OBJECT_NAME => 'NHANVIEN',
        POLICY_NAME => 'AUDIT_UPDATE_LUONG_PHUCAP',
        AUDIT_COLUMN => 'LUONG, PHUCAP',
        AUDIT_CONDITION => 'SYS_CONTEXT(''USERENV'',''SESSION_USER'') != MANV IN (SELECT MANV FROM NHANVIEN WHERE VAITRO = ''TAI CHINH'')',
        STATEMENT_TYPES => 'UPDATE',
        ENABLE => TRUE);
END;