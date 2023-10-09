import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpHelperService } from './http-helper.service';

@Injectable(
    { providedIn: 'root' }
)
export class BaseEntityService {
    private urlGetAll = "GetAll";
    private urlGetById = "GetById";
    private urlAdd = "Add";
    private urlUpdate = "Update";
    private urlDelete = "Delete";
    private urlGetAllLite = "GetAllLite";
    controllerName: string = "";

    constructor(public httpHelperService: HttpHelperService) {
        
    }

    getAll(dataSourceModel: any): any {
        return this.httpHelperService.post(this.controllerName + '/' + this.urlGetAll, dataSourceModel);
    }

    getAllLite(): any {
        return this.httpHelperService.get(this.controllerName + '/' + this.urlGetAllLite);
    }

    getById(id: number): any {
        return this.httpHelperService.get(this.controllerName + '/' + this.urlGetById + '/' + id);
    }

    add(entity: any) {
        return this.httpHelperService.post(`${this.controllerName}/${this.urlAdd}/`, entity);
    }

    update(entity: any) {
        return this.httpHelperService.post(`${this.controllerName}/${this.urlUpdate}/`, entity);
    }

    delete(id: any) {
        return this.httpHelperService.delete(this.controllerName + "/" + this.urlDelete + "/" + id);
    }

    errorHandler(error: Response) {
        return Observable.throw(error);
    }
}
