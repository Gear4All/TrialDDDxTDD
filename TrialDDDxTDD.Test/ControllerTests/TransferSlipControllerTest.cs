using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TrialDDDxTDD.DomainModel;
using TrialDDDxTDD.WebApp.Controllers;
using Microsoft.EntityFrameworkCore;
using TrialDDDxTDD.WebApp.Data;
using System.Net.Http;
using System.Text;

namespace TrialDDDxTDD.Test.ControllerTests
{
    [TestClass]
    public class TransferSlipControllerTest
    {
        [TestMethod]
        public void フロントエンドからリクエストを受け取りそのBodyのjsonデータを振替伝票オブジェクトに変換してDBに保存する()
        {
            DbContextOptionsBuilder<TrialDDDxTDDWebAppContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TrialDDDxTDDWebAppContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase("TrialDDDxTDDWebAppContext");
            TrialDDDxTDDWebAppContext trialDDDxTDDWebAppContext = new TrialDDDxTDDWebAppContext(dbContextOptionsBuilder.Options);
            TransferSlipController transferSlipController = new TransferSlipController(trialDDDxTDDWebAppContext);
            //参考：https://techinfoofmicrosofttech.osscons.jp/index.php?JSON%E3%81%AEparse%E3%82%92%E8%89%B2%E3%80%85%E8%A9%A6%E3%81%97%E3%81%A6%E3%81%BF%E3%81%9F%E3%80%82,
            //https://oita.oika.me/2017/10/22/post-json-with-httpclient/
            var transferSlipJsonStr =
            @"{
                'OccuredDate' : '017-02-01T03:15:45.000Z',
                'Debits' : [
                    { 'SubjectName' : 'Cash',
                    { 'Ammount' : '100' },
                    { 'SubjectName' : 'SavingsAccounts',
                    { 'Ammount' : '100' }
                ],
                'Credits' : [
                    { 'SubjectName' : 'Sale',
                    { 'Ammount' : '200' }
                ]
              }
            ";
            var transferSlipJson = new StringContent(transferSlipJsonStr, Encoding.UTF8, "application/json");

        }
    }
}
