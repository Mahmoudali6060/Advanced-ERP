import { Directive, ElementRef, TemplateRef, ViewContainerRef, Input } from '@angular/core';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { AuthGuardService } from '../guards/auth-guard.service';

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: '[IfPrivileges]'
})
export class IfPrivilegesDirective {

  constructor(
    private element: ElementRef,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authGuardService: AuthGuardService
  ) {
  }

  @Input()
  set IfPrivileges(privilegesIds: Array<number>) {
    debugger;
    for (let val of privilegesIds) {
      if (this.authGuardService.isAuthorize(val)) {
        this.viewContainer.createEmbeddedView(this.templateRef);
        return;
      } 
      else {
        this.viewContainer.clear();
      }
    }
  }
}
