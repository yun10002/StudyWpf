﻿using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string base_url = $"https://openapi.naver.com/v1/search/news.json?start={txtStarNum.Text}&display=10&query={keyword}";
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

            //MessageBox.Show(result);
            var parsedJson = JObject.Parse(result); //string to Json

            int total = Convert.ToInt32(parsedJson["total"]); //전체 검색 결과 수
            int display = Convert.ToInt32(parsedJson["display"]); //10

            //데이터 그리드에 검색 결과 할당
            var items = parsedJson["items"];
            var json_array = (JArray)items;

            List<NewsItem> newsItems = new List<NewsItem>(); //데이터 그리드 연동

            foreach(var item in json_array)
            {
                var temp = DateTime.Parse(item["pubDate"].ToString());
                NewsItem news = new NewsItem()
                {
                    Title = item["title"].ToString(),
                    OriginalLink = item["originallink"].ToString(),
                    Link = item["link"].ToString(),
                    Description = item["description"].ToString(),
                    PubDate = temp.ToString("yyyy-MM-dd HH:mm")
                };

                newsItems.Add(news);
            }

            this.DataContext = newsItems;
        }

        private void dgrResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgrResult.SelectedItem == null) return; //두번 째 검색부터 발생하는 오류 제거

            string link = (dgrResult.SelectedItem as NewsItem).Link;
            Process.Start(link);
        }
    }

    internal class NewsItem
    {
        public string Title { get; set; } 
        public string OriginalLink { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
    }
}
