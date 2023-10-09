import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { environment } from 'src/environments/environment';

@Injectable()

export class ReportService {
  baseUrl: string;
  constructor(private datePipe: DatePipe) {
    this.baseUrl = environment.serverUrl;
  }


  public generateReport(title: any, reportData: any, paidUpTotal: number, balanceTotal: number) {
  //   <div class="header" style="text-align: center;margin: 10px 0 10px 0;">
  //   <img src="`+ this.baseUrl + `Images/report-header.png" width="200" height="130">
  // </div>
    let popupWin:any;
    // printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    var html = `
      <html dir="rtl">
        <head>
          <title>`+ title + `</title>
          <style>
          table { 
            width: 98%; 
            border-collapse: collapse; 
            margin:50px auto;
            margin-top: -9px;
            overflow-x:auto;
            margin-bottom: 0px;
            }
          
          /* Zebra striping */
          tr:nth-of-type(odd) { 
            background: #eee; 
            }
          
          th { 
            background: #bec5c5; 
            color: black; 
            font-weight: bold; 
            }
          
          td, th { 
            padding: 10px; 
            border: 1px solid #ccc; 
            text-align: left; 
            font-size: 18px;
            }
          
          </style>

          <meta charset="utf-8">
          <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
          <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
          <!-- VENDOR CSS -->
          <link rel="stylesheet" href="src/assets/vendor/bootstrap/css/bootstrap.min.css">
          <link rel="stylesheet" href="src/assets/vendor/font-awesome/css/font-awesome.min.css">
          <link rel="stylesheet" href="src/assets/vendor/linearicons/style.css">
          <link rel="stylesheet" href="src/assets/vendor/chartist/css/chartist-custom.css">
          <!-- MAIN CSS -->
          <link rel="stylesheet" href="src/assets/css/main.css">
          <!-- FOR DEMO PURPOSES ONLY. You should remove this in your project -->
          <link rel="stylesheet" href="src/assets/css/demo.css">
          <!-- GOOGLE FONTS -->
          <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700" rel="stylesheet">
          <!-- ICONS -->
          <link rel="apple-touch-icon" sizes="76x76" href="src/assets/img/apple-icon.png">
          <link rel="icon" type="image/png" sizes="96x96" href="src/assets/img/favicon.png">

        </head>
        <body onload="window.print()">
          <div class="wrapper" style="font-family: 'Helvetica Neue', lato, arial, sans-serif;font-size: 12px;">
            <table class="main-table" style="width: 100%;table-layout: fixed;">
              <tr>
                <td></td>
                <td width="1000">
                  
                  <div class="report" style="margin-bottom: 50px;">
                  <table  style="border:none;">
                    <tbody>`;
            html += `<tr>
                        <td style="border:none;">
                        <table  style="border:none;">
                        <tbody>
                          <tr>
                            <td style="border:none;font-size: 20px;
                            font-weight: bold;"> 
                                شـــــركة أبو يــــوسف حجـــازي واخوتــــــه 
                            </td>
                          </tr>
                          <tr>
                          <td style="border:none;font-size: 20px;
                          font-weight: bold;"> 
                               لتوريـــدات الموالـــح (المصانــع والمحطــات)
                          </td>
                        </tr>
                          <tr>
                            <td style="border:none;font-size: 20px;
                            font-weight: bold;"> 
                                جميع أنواع الفرازة (برتقال - ليمون ايضالي)
                            </td>
                          </tr>
                        </tbody>
                      </table>  
                        
                        </td>
                        <td style="border:none;"><img src="`+ this.baseUrl + `Images/report-header.png" width="200" height="130"></td>
                        <td style="border:none;">
                          <table  style="border:none;">
                            <tbody>
                              <tr>
                                <td style="border:none;"> 
                                  م/محمد يـوسف حجــازي :
                                </td>
                                <td style="border:none;">
                                  01017181919
                                </td>
                              </tr>
                              <tr>
                                <td style="border:none;"> 
                                     احمد يــــوسف حجــازي :
                                </td>
                                <td style="border:none;">
                                  01280798977
                                </td>
                              </tr>
                              <tr>
                              <td style="border:none;"> 
                                   مصطفى يوسف حجازي :
                              </td>
                              <td style="border:none;">
                                01110989921
                              </td>
                            </tr>
                            </tbody>
                          </table>  
                         </td>
                      </tr>`;
            html += `</tbody>`;
          html += `</table>`;

                  html += `<table class="table table-bordered">
                    <thead>
                      <tr>`
    html += `<th stye="background: #bec5c5;color: black;ont-weight: bold; ">اجمالي المدفوعات </th>`;
    html += `<th >اجمالي الرصيد</th>`;
    html += `<th >الاجمالي </th>`;
    html += `</tr>
                    </thead>
                    <tbody>`;
    html += `<tr>
                          <td>`+ paidUpTotal.toFixed(2) + `</td>
                          <td >`+ balanceTotal.toFixed(2) + `</td>
                          <td >`+ (paidUpTotal - balanceTotal).toFixed(2) + `</td>
                        </tr>`;
    html += `</tbody>`;
    html += `</table>`;
    for (let data of reportData) {
      html += `<h2 style="font-weight: 300;margin-left: 100%;">` + data.title + ` </h2>
                      <table class="table table-bordered">
                        <thead>
                          <tr>`
      for (let header of data.headers) {
        html += `<th stye="background: #bec5c5;color: black;font-weight: bold;">` + header + `</th>`;
      }
      html += `</tr>
                        </thead>
                        <tbody>`;
      for (let row of data.list) {
        html += `<tr>`;
        for (let property of data.properties) {
          html += `<td >`;
          if (property.includes('Date')) {
            html += this.datePipe.transform(row[property], 'yyyy/MM/dd')
          }
          else {
            html += row[property]
          }

          + `</td>`;
        }
        html += `</tr>`;
      }
      html += `</tbody>`;
      html += `</table>`;
    }
    html += `</td>
                <td></td>
              </tr>
            </table>
          </div>
        </body>
      </html>`
    popupWin.document.write(html);
    popupWin.document.close();
  }

}