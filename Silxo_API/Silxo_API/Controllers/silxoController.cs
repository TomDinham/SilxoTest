using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Silxo_API;

namespace WebService.Controllers
{

    [EnableCors(origins: "*", headers: "*",methods:"*")]
    public class silxoController : ApiController
    {
        MySqlConnection connection;
        MySqlCommand command;
        Questions questions = new Questions();

        silxoController()
        {
            connection = new MySqlConnection();
            connection.ConnectionString = @"server=localhost;userid=root;password=pass;database = sys";
            command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

          

        }
        public string GetQuestions()
        {
            try
            {
                var dataSet = new DataSet();
                var answers = new DataSet();

                command.CommandText = "SELECT * from sys.question ";
                command.Parameters.Add("@QR", MySqlDbType.Int32);
                connection.Open();

                var dataAdapter = new MySqlDataAdapter { SelectCommand = command };

                dataAdapter.Fill(dataSet);



                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Question question = new Question();
                        question.question = row["Question"].ToString();
                        question.type = row["Type"].ToString();
                        question.refrence = row["Ref"].ToString();


                        var QuestionRef = question.refrence;
                        command.CommandText = "SELECT sys.answer.Answer, sys.answer.Score FROM sys.answer Where sys.answer.QuestionRef =  @QR ";

                        command.Parameters["@QR"].Value = QuestionRef;


                        dataAdapter = new MySqlDataAdapter { SelectCommand = command };
                        answers.Clear();
                        dataAdapter.Fill(answers);

                        foreach (DataTable ATable in answers.Tables)
                        {

                            foreach (DataRow ARow in ATable.Rows)
                            {
                                Answer ans = new Answer();
                                ans.answer = ARow["Answer"].ToString();
                                ans.Score = ARow["Score"].ToString();


                                question.AddAnswer(ans);

                            }
                        }
                        questions.AddQuestions(question);
                    }
                }
                string JSON = JsonConvert.SerializeObject(questions);
                return JSON;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != connection)
                {
                    connection.Close();
                }
            }
        }
        //public string Get()
        //{
        //    try
        //    {

        //        var dataSet = new DataSet();
        //        var answers = new DataSet();
        //        command.CommandText = "SELECT * from sys.question ";
        //        command.Parameters.Add("@QR", MySqlDbType.Int32);
        //        connection.Open();

        //        var dataAdapter = new MySqlDataAdapter { SelectCommand = command };

        //        dataAdapter.Fill(dataSet);

        //        foreach (DataTable table in dataSet.Tables)
        //        {
        //            foreach (DataRow row in table.Rows)
        //            {
        //                data = data + "{\"Question\":\"" + row["Question"] + "\",";
        //                data = data + "\"type\":" + row["Type"] + ",";
        //                data = data + "\"Answers\":[";



        //                var QuestionRef = row["Ref"];
        //                command.CommandText = "SELECT sys.answer.Answer, sys.answer.Score FROM sys.answer Where sys.answer.QuestionRef =  @QR ";

        //                command.Parameters["@QR"].Value = QuestionRef;


        //                dataAdapter = new MySqlDataAdapter { SelectCommand = command };
        //                answers.Clear();
        //                dataAdapter.Fill(answers);

        //                foreach (DataTable ATable in answers.Tables)
        //                {
        //                    foreach (DataRow ARow in ATable.Rows)
        //                    {
        //                        data = data + "{\"Answer\" :\" " + ARow["answer"] + "\",";
        //                        data = data + "\"Score\" : " + ARow["score"] + "},";
        //                    }
        //                    data = data.TrimEnd(',');
        //                    data = data + "]";
        //                }
        //                data = data + "},";
        //            }
        //            data = data.TrimEnd(',');
        //            data = data + "]}";
        //        }

        //        data = data.TrimEnd(',');

        //        data = JsonConvert.SerializeObject(data);
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //    finally
        //    {
        //        if (null != connection)
        //        {
        //            connection.Close();
        //        }
        //    }

        //}
        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message")
            };
        }
        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }
    }
}
