

-- Dang nhap bang quanly va chay lan luot cac cau lenh sau
alter system set audit_trail = DB,EXTENDED scope = spfile;
shutdown immediate;
startup;

-- Theo doi hanh vi cua cac user tren tat ca cac table
AUDIT ALL ON QUANLY.NHANVIEN BY ACCESS;
AUDIT ALL ON QUANLY.PHANCONG BY ACCESS;
AUDIT ALL ON QUANLY.DEAN BY ACCESS;
AUDIT ALL ON QUANLY.PHONGBAN BY ACCESS;
AUDIT ALL ON QUANLY.NHANVIENQUANLY BY ACCESS;
AUDIT ALL ON QUANLY.NHANVIENPHONGBAN BY ACCESS;


-- Theo doi cac hanh vi thanh cong
AUDIT ALL WHENEVER SUCCESSFUL;

-- Theo doi cac hanh vi khong thanh cong
AUDIT ALL WHENEVER NOT SUCCESSFUL;

--------------------AUDIT TESTING--------------------
--B1: dang nhap bang user NV008 va chay cau lenh sau:
SELECT * FROM quanly.NHANVIEN;

-- B2: dang nhap bang quanly, chay cau lenh sau de xem ket qua audit:
select username, EXTENDED_TIMESTAMP ,obj_name, action_name, sql_text 
from dba_audit_trail
WHERE OBJ_NAME = 'BENHNHAN';