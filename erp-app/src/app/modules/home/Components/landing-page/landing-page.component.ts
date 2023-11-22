import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { AboutUs } from 'src/app/modules/configurations/models/about-us';
import { AdvertismentDTO } from 'src/app/modules/configurations/models/advertisment-dto';
import { ContactUs } from 'src/app/modules/configurations/models/contact-us';
import { AboutUsService } from 'src/app/modules/configurations/services/about-us.service';
import { AdvertismentService } from 'src/app/modules/configurations/services/advertisment.service';
import { ContactUsService } from 'src/app/modules/configurations/services/contact-us.service';
import { ConfigService } from 'src/app/shared/services/config.service';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  isLogin: boolean;
  mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";
  contactUs: ContactUs;
  aboutUsSearch: AboutUs;
  aboutUsList: Array<AboutUs>;
  products: any[];
  lstAdvertisment: AdvertismentDTO[]
  responsiveOptions: any;
  ads: AdvertismentDTO
  serverUrl: any;
  constructor(
    private _configService: ConfigService,
    private advertismentService: AdvertismentService,
    private aboutUsService: AboutUsService,
    private contactUsService: ContactUsService,
    private router: Router,
    private toasterService: ToastrService,
    private translate: TranslateService) {

    this.responsiveOptions = [
      {
        breakpoint: '1024px',
        numVisible: 3,
        numScroll: 3
      },
      {
        breakpoint: '768px',
        numVisible: 2,
        numScroll: 2
      },
      {
        breakpoint: '560px',
        numVisible: 1,
        numScroll: 1
      }
    ];
  }

  ngOnInit(): void {
    this.isLogin = false;
    this.contactUs = new ContactUs();
    this.aboutUsSearch = new AboutUs();
    this.getAboutUs();
    this.lstAdvertisment = [];
    this.ads = new AdvertismentDTO();
    this.getAdvertisments();
  }
  openRequestForm() {
    this.router.navigate(["/CarRequest"]);
  }
  save(frm: NgForm) {
    if (this.validattion(this.contactUs)) {
      this.contactUsService.add(this.contactUs).subscribe(res => {
        this.contactUs = new ContactUs();
        this.toasterService.success("success");
      })
    }
  }
  getAboutUs() {
    this.aboutUsService.getAll(this.aboutUsSearch).subscribe((res: any) => {
      this.aboutUsList = res.list;
    });
  }
  getAdvertisments() {
    this.advertismentService.getAll(this.ads).subscribe((res: any) => {
      this.lstAdvertisment = res.list;
      this.serverUrl = this._configService.getServerUrl();
    });
  }
  validattion(contactUs: ContactUs): boolean {
    if (!contactUs.name || isNullOrUndefined(contactUs.name)) {
      this.toasterService.error(this.translate.instant("Errors.NameIsRequired"));
      return false;
    }
    if (!contactUs.mobile || isNullOrUndefined(contactUs.mobile)) {
      this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
      return false;
    }
    if (!contactUs.email || isNullOrUndefined(contactUs.email)) {
      this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
      return false;
    }
    return true;
  }
}
