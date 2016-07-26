using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCCom.MongoDB
{
    public class MongoHelper
    {

        String _ConnectionString { get; set; }
        String _DataBaseName { get; set; }
        String _TableName { get; set; }
        public MongoHelper(String ConnectionString, String DataBaseName, String TableName)
        {
            _ConnectionString = ConnectionString;
            _DataBaseName = DataBaseName;
            _TableName = TableName;
        }
        public Int32 MongoDBInsert<T>(T t)
        {
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取指定数据库
            MongoDatabase db = server.GetDatabase(_DataBaseName);
            //获取表
            MongoCollection<T> col = db.GetCollection<T>(_TableName);
            try
            {
                if (col == null) return -1;
                //插入
                col.Insert(t);
                return 1;
            }
            catch { return -1; }
            finally { server.Disconnect(); }
        }
        public Int32 MongoDBDelete<T>(QueryDocument QueryDoc)
        {
            //创建数据连接
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取指定数据库
            MongoDatabase db = server.GetDatabase(_DataBaseName);
            //获取表
            MongoCollection<T> col = db.GetCollection<T>(_TableName);
            try
            {
                if (col == null) return -1;
                col.Remove(QueryDoc);
                return 1;
            }
            catch { return -1; }
            finally { server.Disconnect(); }
        }
        public Int32 MongoDBUpDate<T>(T t, QueryDocument QueryDoc)
        {
            //创建数据连接
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取指定数据库
            MongoDatabase db = server.GetDatabase(_DataBaseName);
            //获取表
            MongoCollection<T> col = db.GetCollection<T>(_TableName);
            try
            {

                if (col == null) return -1;
                BsonDocument bd = BsonExtensionMethods.ToBsonDocument(t);
                col.Update(QueryDoc, new UpdateDocument(bd));
                return 1;
            }
            catch { return -1; }
            finally { server.Disconnect(); }
        }
        public List<T> MongoDBQuery<T>(QueryDocument QueryDoc)
        {
            //创建数据连接
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取指定数据库
            MongoDatabase db = server.GetDatabase(_DataBaseName);
            //获取表
            MongoCollection<T> col = db.GetCollection<T>(_TableName);
            try
            {
                if (col == null) return null;
                return col.FindAs<T>(QueryDoc).ToList();
            }
            catch { return null; }
            finally { server.Disconnect(); }
        }

        public List<T> MongoDBQuery<T>()
        {
            //创建数据连接
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取指定数据库
            MongoDatabase db = server.GetDatabase(_DataBaseName);
            //获取表
            MongoCollection<T> col = db.GetCollection<T>(_TableName);
            try
            {
                if (col == null) return null;
                return col.FindAllAs<T>().ToList();
            }
            catch { return null; }
            finally { server.Disconnect(); }
        }


        public  MongoCursor<BsonDocument> Search( IMongoQuery query)
        {
            //定义Mongo服务  
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(_DataBaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(_TableName);
            try
            {
                if (query == null)
                    return collection.FindAll();
                else
                    return collection.Find(query);
            }
            finally
            {
                server.Disconnect();
            }
        }
        /// <summary>  
        /// 新增  
        /// </summary>   
        public  Boolean Insert( BsonDocument document)
        {

            //定义Mongo服务  
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(_DataBaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(_TableName);
            try
            {
                collection.Insert(document);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }
        /// <summary>  
        /// 修改  
        /// </summary>    
        public  Boolean Update(IMongoQuery query, IMongoUpdate new_doc)
        {
            //定义Mongo服务  
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(_DataBaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(_TableName);
            try
            {
                collection.Update(query, new_doc);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }
        /// <summary>  
        /// 移除  
        /// </summary>  
        public  Boolean Remove( IMongoQuery query)
        {
            //定义Mongo服务  
            MongoServer server = MongoServer.Create(_ConnectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(_DataBaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(_TableName);
            try
            {
                collection.Remove(query);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }  

        //       public List<T> MongoDBAll<T>(T t)
        //       {
        //           try
        //           {
        //               if (Collection == null) return -1;
        //               BsonDocument bd = BsonExtensionMethods.ToBsonDocument(t);

        //               IMongoQuery query = Query.EQ("_id", (new YCCom.Tool.T_Reflect()).getPropertyValue(t, "Id").ToString());

        //               Collection.Update(query, new UpdateDocument(bd));
        //               return 1;
        //           }
        //           catch { return -1; }
        //       }

        //       Query.All("name", "a", "b");//通过多个元素来匹配数组

        //Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件

        //Query.EQ("name", "a");//等于

        //Query.Exists("type", true);//判断键值是否存在

        //Query.GT("value", 2);//大于>

        //Query.GTE("value", 3);//大于等于>=

        //Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值

        //Query.LT("value", 9);//小于<

        //Query.LTE("value", 8);//小于等于<=

        //Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果

        //Query.NE("name", "c");//不等于

        //Query.Nor(Array);//不包括数组中的值

        //Query.Not("name");//元素条件语句

        //Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档

        //Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件

        //Query.Size("name", 2);//给定键的长度

        //Query.Type("_id", BsonType.ObjectId );//给定键的类型

        //Query.Where(BsonJavaScript);//执行JavaScript

        //Query.Matches("Title",str);//模糊查询 相当于sql中like -- str可包含正则表达式
    }
}
