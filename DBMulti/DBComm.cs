using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBMulti
{

    /// <summary>
    /// 패키지가 필요함
    /// ServiceStack.OrmLite.MySql
    /// 
    /// 로컬에서 작업할 때는 MySQL에 차라리 패스워드가 없는 편이 편할 수도 있다. 
    /// mysql 설정 파일의 [mysqld] 섹션에 skip-grant-tables라고 문구를 추가한다.
    /// 설정파일은  C:\Program Files\MariaDB 10.5\data\my.ini
    /// https://github.com/ServiceStack/ServiceStack.OrmLite
    /// </summary>
    public class DBComm<T> 
    {
        public string ConnectString => $"Server={DbServer};Port={DbPort};Database={DbName};";

        public string DbServer { get; set; } = "localhost";
        public int DbPort { get; set; } = 3306;
        public string DbName { get; set; } = "Test";

        protected OrmLiteConnectionFactory dbFactory = null;
        public bool IsInitialized { get; private set; } = false;

        public DBComm()
        {
            InitDB();
        }

        public void InitDB()
        {
            try
            {
                dbFactory = new OrmLiteConnectionFactory(
                        ConnectString,
                        MySqlDialect.Provider);

                using (var db = dbFactory.Open())
                {
                    db.CreateTableIfNotExists<T>();
                }
                IsInitialized = true;
            }
            catch (Exception ex)
            {
                IsInitialized = false;
                // 로그 시스템이 초기화되기 전일 수 있으므로 콘솔이나 디버그 출력을 사용
                System.Diagnostics.Debug.WriteLine($"DB Initialization Failed: {ex.Message}");
            }
        }

        public void CreateTableIfNotExist()
        {
            if (!IsInitialized) return;
            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<T>();
            }
        }

        public void CreateTable()
        {
            if (!IsInitialized) return;
            using (var db = dbFactory.Open())
            {
                db.DropAndCreateTable<T>();
            }
        }

        public List<T> Select()
        {
            if (!IsInitialized) return new List<T>();
            using (var db = dbFactory.Open())
            {
                return db.Select<T>();
            }
        }

        public List<T> Select(string sql)
        {
            if (!IsInitialized) return new List<T>();
            using (var db = dbFactory.Open())
            {
                return db.Select<T>(sql);
            }
        }

        public List<T> Select(Expression<Func<T, bool>> expression)
        {
            if (!IsInitialized) return new List<T>();
            using (var db = dbFactory.Open())
            {
                return db.Select<T>(expression);
            }
        }

        public T Single(Expression<Func<T, bool>> expression)
        {
            if (!IsInitialized) return default(T);
            using (var db = dbFactory.Open())
            {
                return db.Single<T>(expression);
            }
        }
    }

 }
