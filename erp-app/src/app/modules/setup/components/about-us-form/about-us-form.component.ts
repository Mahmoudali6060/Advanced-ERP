import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { AboutUs } from '../../models/about-us';
import { AboutUsService } from '../../services/about-us.service';

@Component({
  selector: 'app-about-us-form',
  templateUrl: './about-us-form.component.html',
  styleUrls: ['./about-us-form.component.css']
})

export class AboutUsFormComponent implements OnInit {

  aboutUs : AboutUs;
  constructor(private aboutUsService: AboutUsService ,private router: Router,
    private toasterService: ToastrService,private translate: TranslateService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.aboutUs = new AboutUs();
    const id = this.route.snapshot.paramMap.get('id');
    this.getAboutUs(id);
  }
  getAboutUs(id: any) {
    this.aboutUsService.getById(id).subscribe((res: any) => {
      this.aboutUs = res;
    });  
  }
	cancel() {
		this.router.navigateByUrl('setup/about-us');
	}
  save(frm: NgForm) {
			if (this.aboutUs.id) {
				this.aboutUsService.update(this.aboutUs).subscribe(res => {
					this.toasterService.success("success");
					this.cancel();
				})
			}
	}
}
