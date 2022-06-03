﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfNaverNewsSearch
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Commons.ShowMessageAsync("실행", "뉴스 검색 실행!");
                SearchNaverNews();
            }
        }

        private void SearchNaverNews()
        {
            string keyword = txtSearch.Text;
            string clientID = "GgcvZkb5HvsAXK4ElD6e";
            string clientSeceret = "RNEwrV1xpS";
            string base_url = $"https://openapi.naver.com/v1/search/news.json?start=1&display=100&query={keyword}";
            string result;

            WebRequest request = null;
            WebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;

            // Naver OpenAPI 실제 요청
            try
            {
                request = WebRequest.Create(base_url);
                request.Headers.Add("X-Naver-Client-Id", clientID); //중요!!!@@ 얜 고정임
                request.Headers.Add("X-Naver-Client-Secret", clientSeceret); //중요2@@ 고정2@@

                response = request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);

                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                stream.Close();
                response.Close();
            }

            MessageBox.Show(result);
            //var parsedJson = JObject.Parse(result); //string to Json
        }
    }
}
