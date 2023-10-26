import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RoleService } from '../../../services/role.service';
import { Location } from '@angular/common';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { HelperService } from 'src/app/shared/services/helper.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { isNullOrUndefined } from 'util';
import { RoleDTO } from '../../../models/role.dto';

@Component({
	selector: 'app-role-form',
	templateUrl: './role-form.component.html',
	styleUrls: ['./role-form.component.css']
})
export class RoleFormComponent {

	roleDTO: RoleDTO = new RoleDTO();
	viewMode: boolean;
	constructor(private roleService: RoleService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private location: Location, private _configService: ConfigService,
		private helperService: HelperService,
		private translate: TranslateService,
		private subjectService: SubjectService,
		private localStorageService: LocalStorageService,
		private router: Router) {
	}

	ngOnInit() {
		this.roleDTO = new RoleDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getRoleById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}
	getRoleById(roleId: any) {
		this.roleService.getById(roleId).subscribe((res: any) => {
			this.roleDTO = res;
		})
	}

	cancel() {

		this.router.navigateByUrl('role/role-list');
	}
	validation(roleDTO: RoleDTO): boolean {
		if (!roleDTO.name || isNullOrUndefined(roleDTO.name)) {
			this.toasterService.error(this.translate.instant("Errors.ThisFieldIsRequired"));
			return false;
		}
		return true;
	}
	save(frm: NgForm) {
		if (this.validation(this.roleDTO)) {
			if (this.roleDTO.id) {
				this.roleService.update(this.roleDTO).subscribe(res => {
					this.toasterService.success("success");
					this.cancel();
				})
			}
			else {
				this.roleService.add(this.roleDTO).subscribe(res => {
					this.toasterService.success("success");
					this.cancel();
				})
			}
		}
	}

	

}
