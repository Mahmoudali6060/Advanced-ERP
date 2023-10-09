import { Injectable } from '@angular/core';
// import { UserProfile } from '../model/user-profile.model';
// import { SubHeader } from '../model/sub-header.model';
// import { Accounting } from '../model/accounting.model';
// import { Store } from '../model/store.model';
// import { Client } from '../model/client.model';
// import { PrivilegeEnum } from '../enums/privilege.enum';
// import { Maintenance } from '../model/maintenance.model';
// import { PrimengLocal } from '../model/primeng-local.model';


@Injectable({ providedIn: 'root' })
export class FixedService {
  public themeLoaded: boolean;
  public activeLang: any = {};
  public activeModule: any = {};
  public appLanguages: any = [];
  // public userProfile: UserProfile;
  // public subHeader: SubHeader;
  public menuList: any;
  public menuListTree: any;
  public recordPerPage: any = [];
  public KTAppOptions: any;
  public datePickerConfig: any = {};
  public dateTimePickerConfig: any = {};
  // public accounting: Accounting;
  // public store: Store;
  // public maintenance: Maintenance;
  public currentURL: string | undefined;
  // public client: Client;
  public today: any;
  // public PrivilegeEnum = PrivilegeEnum;
  public countryList: any = [];
  // public primengLocal: PrimengLocal;

  constructor() {
    // this.accounting = new Accounting();
    // this.store = new Store();
    // this.maintenance = new Maintenance();
    // this.userProfile = new UserProfile();
    // this.subHeader = new SubHeader();
    this.initialAppLang();
    this.themeLoaded = false;
    this.today = new Date();
    this.recordPerPage = [
      { label: '10', value: 10 },
      { label: '20', value: 20 },
      { label: '30', value: 30 },
      { label: '40', value: 40 },
      { label: '50', value: 50 },
      { label: '100', value: 100 },
      { label: '150', value: 150 },
      { label: 'Common.All', value: 0 }
    ];
    this.KTAppOptions = {
      colors: {
        state: {
          brand: '#5d78ff',
          dark: '#282a3c',
          light: '#ffffff',
          primary: '#5867dd',
          success: '#34bfa3',
          info: '#36a3f7',
          warning: '#ffb822',
          danger: '#fd3995'
        },
        base: {
          label: ['#c5cbe3', '#a1a8c3', '#3d4465', '#3e4466'],
          shape: ['#f0f3ff', '#d9dffa', '#afb4d4', '#646c9a']
        }
      }
    };
    this.datePickerConfig = {
      dateInputFormat: 'YYYY/MM/DD',
      containerClass: 'theme-dark-blue',
      showWeekNumbers: false
    };
    this.dateTimePickerConfig = {
      dateInputFormat: 'YYYY/MM/DD , hh:mm a',
      containerClass: 'theme-dark-blue',
      showWeekNumbers: false
    };
    // this.primengLocal = new PrimengLocal();
    // this.primengLocal.en = {
    //   firstDayOfWeek: 6,
    //   dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
    //   dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
    //   dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
    //   monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August',
    //     'September', 'October', 'November', 'December'],
    //   monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    //   today: 'Today',
    //   clear: 'Clear',
    //   dateFormat: 'YYYY/MM/DD , hh:mm a',
    //   weekHeader: 'Wk'
    // };
    // this.primengLocal.ar = {
    //   firstDayOfWeek: 6,
    //   dayNames: ['الأحد', 'الإثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
    //   dayNamesShort: ['أحد', 'إثنين', 'ثلاثاء', 'أربعاء', 'خميس', 'جمعة', 'سبت'],
    //   dayNamesMin: ['أحد', 'إثنين', 'ثلاثاء', 'أربعاء', 'خميس', 'جمعة', 'سبت'],
    //   monthNames: ['يناير', 'فبراير', 'مارس', 'إبريل', 'مايو', 'يونيو', 'يوليو', 'أغسطس',
    //     'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر'],
    //   monthNamesShort: ['يناير', 'فبراير', 'مارس', 'إبريل', 'مايو', 'يونيو', 'يوليو', 'أغسطس',
    //     'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر'],
    //   today: 'اليوم',
    //   clear: 'مسح',
    //   dateFormat: 'YYYY/MM/DD , hh:mm a',
    //   weekHeader: 'اسبوع'
    // };
  }

  initialAppLang() {
    this.appLanguages = [
      { languageId: 1, isRTL: true, code: 'ar', name: 'عربي', dir: 'rtl', icon: 'flag-icon-sa' },
      { languageId: 2, isRTL: false, code: 'en', name: 'English', dir: 'ltr', icon: 'flag-icon-uk' }
    ];
  }
}
