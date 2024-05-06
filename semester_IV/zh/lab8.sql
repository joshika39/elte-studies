

-- 4
/* Cursor (több soros SELECT)
Írjunk meg egy procedúrát, amelyik veszi a paraméterül megadott osztály dolgozóit ábécé 
szerinti sorrendben, és kiírja a foglalkozásaikat egy karakterláncban összefűzve.
*/
-- CREATE OR REPLACE PROCEDURE print_jobs(o_nev varchar2) IS
-- Tesztelés:
set serveroutput on
execute print_jobs('RESEARCH');  /* példa output: CLERK-ANALYST-MANAGER-ANALYST-CLERK  */
