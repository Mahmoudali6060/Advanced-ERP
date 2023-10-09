import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { AdvertismentDTO } from '../../models/advertisment-dto';
import { AdvertismentService } from '../../services/advertisment.service';

@Component({
  selector: 'app-advertisment-form',
  templateUrl: './advertisment-form.component.html',
  styleUrls: ['./advertisment-form.component.css']
})
export class AdvertismentFormComponent implements OnInit {
  imageSrc: string;
  ads: AdvertismentDTO
  lstAdvertisment: AdvertismentDTO[]
  editMode: boolean;
  serverUrl: string;
  id: any;
  constructor(private router: Router, private route: ActivatedRoute, private _configService: ConfigService, private advertismentService: AdvertismentService, private toasterService: ToastrService,) { }

  ngOnInit(): void {
    this.ads = new AdvertismentDTO();
    this.lstAdvertisment = [];
     this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.getById(this.id);
      if (this.router.url.includes('/edit-advertisment/')) {
        this.editMode = true;
      }
    }
  }
  getById(id: any) {
    this.advertismentService.getById(id).subscribe((res: any) => {
      this.ads = res;
      this.serverUrl = this._configService.getServerUrl();
      this.imageSrc = this.serverUrl + "wwwroot/Images/Managment/" + this.ads.media;
    });
  }

  onFileChange(event: any) {

    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.ads.mediaBase64 = this.imageSrc;
        if(!this.id){
          this.lstAdvertisment.push(this.ads);
          this.ads = new AdvertismentDTO();
        }
      };
    }
  }
  save() {
    this.advertismentService.addRang(this.lstAdvertisment).subscribe(res => {
      this.toasterService.success("success");
      this.cancel();
    })
  }
  update(){
    this.advertismentService.update(this.ads).subscribe(res => {
      this.toasterService.success("success");
      this.cancel();
    })
  }
  delete(item:any) {
    this.lstAdvertisment.forEach((element,index)=>{
			if(element==item) this.lstAdvertisment.splice(index,1);
		 });
  }
  cancel() {
		this.router.navigateByUrl('setup/advertisment');
	}
}
