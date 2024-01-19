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
import { RoleDTO } from '../../../models/role.dto';
import { RolePrivilegeDTO } from '../../../models/privilege-dto';
import { Node } from '../../../models/node-dto';
import { PrivilegeDATA } from '../../../mock-data/privilege-data';

@Component({
	selector: 'app-role-form',
	templateUrl: './role-form.component.html',
	styleUrls: ['./role-form.component.css']
})
export class RoleFormComponent {

	roleDTO: RoleDTO = new RoleDTO();
	viewMode: boolean;
	privileges: any = [];
	selectedPrivileges: Array<RolePrivilegeDTO> = new Array<RolePrivilegeDTO>();

	constructor(private roleService: RoleService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private location: Location,
		private _configService: ConfigService,
		public helperService: HelperService,
		private translate: TranslateService,
		private subjectService: SubjectService,
		private localStorageService: LocalStorageService,
		private router: Router
	) {

	}

	ngOnInit() {
		this.privileges = JSON.parse(JSON.stringify(PrivilegeDATA));
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
			if (this.roleDTO.rolePrivileges) {
				this.selectedPrivileges = this.roleDTO.rolePrivileges;
				this.setCheckedItems();
				this.checkCategoryAndPage();
			}
		})
	}

	cancel() {

		this.router.navigateByUrl('user/role-list');
	}
	validation(roleDTO: RoleDTO): boolean {
		// if (!roleDTO.name || isNullOrUndefined(roleDTO.name)) {
		// 	this.toasterService.error(this.translate.instant("Errors.ThisFieldIsRequired"));
		// 	return false;
		// }
		return true;
	}
	save(frm: NgForm) {
		this.roleDTO.rolePrivileges = this.selectedPrivileges;
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

	setCheckedItems() {
		for (let item of this.selectedPrivileges) {
			for (let category of this.privileges) {
				for (let page of category.children) {
					for (let privilege of page.children) {
						if (item.privilegeId == privilege.id)
							privilege.checked = true;
					}
				}
			}
		}
	}
	select(item: Node) {
		item.checked = !item.checked;
		this.setPrivilege(item);
		for (let page of item?.children) {
			page.checked = item.checked;
			this.setPrivilege(page);
			for (let privilege of page?.children) {
				privilege.checked = item.checked;
				this.setPrivilege(privilege);
			}
		}
		this.checkCategoryAndPage();

	}

	checkCategoryAndPage() {
		for (let category of this.privileges) {
			for (let page of category.children) {
				page.checked = page.children.every((x: any) => x.checked == true);
			}
			category.checked = category.children.every((x: any) => x.checked == true);
		}
	}

	setPrivilege(item: Node) {
		if (item.level == 2) {
			if (item.checked) {
				let exsitedItem = this.selectedPrivileges.find(x => x.privilegeId == item.id);
				if (!exsitedItem) {
					let rolePrivilege: RolePrivilegeDTO = new RolePrivilegeDTO();
					rolePrivilege.privilegeId = item.id;
					this.selectedPrivileges.push(rolePrivilege);
				}
			}
			else
				this.selectedPrivileges = this.selectedPrivileges.filter(x => x.privilegeId != item.id);
		}
	}



}


