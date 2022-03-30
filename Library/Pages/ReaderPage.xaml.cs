using Library.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReaderPage.xaml
    /// </summary>
    public partial class ReaderPage : Page
    {
        public Reader selectedClient { get; set; }
        List<RecordBook> booksByReaders = new List<RecordBook>();
        LibraryCard libraryCard;
        string token { get; set; }

        public ReaderPage(Reader selectedClient, string token, LibraryCard libraryCard)
        {
            InitializeComponent();
            this.DataContext = this;
            this.token = token;
            this.libraryCard = libraryCard;
            DgReaderBook.ItemsSource = libraryCard.Records;
        }

        private void word_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Print(libraryCard);
        }

        private void pdf_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void xl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ReadersList(token));
        }
        public void CountSt(string sortGen = "Отобразить всё", string sortAut = "Отобразить всё", string sortPub = "Отобразить всё")
        {
            var list = libraryCard.Records;
            if (sortGen != "Отобразить всё")
            {
                list = list.Where(x => x.book.genre == sortGen).ToList();
            }

            if (sortAut != "Отобразить всё")
            {
                list = list.Where(x => x.book.author == sortAut).ToList();
            }
            if (sortPub != "Отобразить всё")
            {
                list = list.Where(x => x.book.publisherEx == sortPub).ToList();
            }
            DgReaderBook.ItemsSource = list;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Properties.Settings.Default.BaseAddress) })
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
            }
            List<string> genres = new List<string>();
            genres.Add("Отобразить всё");
            genres.AddRange((DgReaderBook.ItemsSource as List<RecordBook>).Select(b => b.book.genre).Distinct().ToList());
            cbGenre.ItemsSource = genres;

            List<string> authors = new List<string>();
            authors.Add("Отобразить всё");
            authors.AddRange((DgReaderBook.ItemsSource as List<RecordBook>).Select(b => b.book.author).Distinct().ToList());
            cbAuthor.ItemsSource = authors;

            List<string> publishers = new List<string>();
            publishers.Add("Отобразить всё");
            publishers.AddRange((DgReaderBook.ItemsSource as List<RecordBook>).Select(b => b.book.publisherEx).Distinct().ToList());
            cbPublisher.ItemsSource = publishers;

            cbGenre.SelectedIndex = 0;
            cbAuthor.SelectedIndex = 0;
            cbPublisher.SelectedIndex = 0;

            cbGenre.SelectionChanged += cbGenre_SelectionChanged;
            cbAuthor.SelectionChanged += cbAuthor_SelectionChanged;
            cbPublisher.SelectionChanged += cbPublisher_SelectionChanged;
            CountSt(cbGenre.Text, cbAuthor.Text, cbPublisher.Text);
        }

        private void cbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountSt(cbGenre.SelectedItem.ToString(), cbAuthor.Text, cbPublisher.Text);
        }

        private void cbPublisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountSt(cbGenre.Text, cbAuthor.Text, cbPublisher.SelectedItem.ToString());
        }

        private void cbAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountSt(cbGenre.Text, cbAuthor.SelectedItem.ToString(), cbPublisher.Text);
        }

        async System.Threading.Tasks.Task Print(LibraryCard libraryСard)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить файл как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            if (savedialog.ShowDialog() == true)
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    var word = new Word.Application();
                    var document = word.Documents.Open(Environment.CurrentDirectory + @"\список_книг_template.docx");
                    DateTime date = DateTime.Now;
                    try
                    {
                        var table1 = document.Tables[1];
                        int row = 1;
                        foreach (var item in libraryCard.Records)
                        {

                            row++;
                            table1.Rows.Add();
                            table1.Cell(row, 1).Range.Text = (row - 1).ToString();
                            table1.Cell(row, 2).Range.Text = item.retDay.ToString();
                            table1.Cell(row, 3).Range.Text = item.book.authorTitle;
                            table1.Cell(row, 4).Range.Text = item.book.publisherEx;
                            string mark = "";
                            if (item.retDay > 7)
                            {
                                mark = "сдана не вовремя";
                            }
                            else
                            {
                                mark = "сдана вовремя";
                            }
                            table1.Cell(row, 5).Range.Text = mark;
                        }

                        int T = table1.Rows.Count;
                        table1.Rows[T].Delete();
                        document.SaveAs2(savedialog.FileName, Word.WdSaveFormat.wdFormatDocument, Word.WdSaveOptions.wdDoNotSaveChanges);
                        document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                        word.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                        MessageBox.Show("Отчёт успешно создан!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                        word.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                    }
                });
            }
        }
    }
}
