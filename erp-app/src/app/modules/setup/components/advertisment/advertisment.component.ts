import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { AdvertismentDTO } from '../../models/advertisment-dto';
import { AdvertismentSearchDTO } from '../../models/advertisment-search-dto';
import { AdvertismentService } from '../../services/advertisment.service';

@Component({
  selector: 'app-advertisment',
  templateUrl: './advertisment.component.html',
  styleUrls: ['./advertisment.component.css']
})
export class AdvertismentComponent implements OnInit {
  lstAdvertisment: AdvertismentDTO[]
  advertismentSearchDTO: AdvertismentSearchDTO
  serverUrl: string;

  constructor(private translate: TranslateService,
    private toastrService:ToastrService,
    private router: Router, 
    private route: ActivatedRoute, 
    private _configService: ConfigService, 
    private advertismentService: AdvertismentService) { }

  ngOnInit(): void {
    this.lstAdvertisment = [];
    this.advertismentSearchDTO = new AdvertismentSearchDTO();
    this.getAll();
  }
  getAll(){
    this.advertismentService.getAll(this.advertismentSearchDTO).subscribe((res: any) => {
      this.lstAdvertisment = res.list;
			this.serverUrl = this._configService.getServerUrl();

    });
  }
  delete(item:AdvertismentDTO){
		this.advertismentService.delete(item.id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAll();
			}
		});
  }
}
