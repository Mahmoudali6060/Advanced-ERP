import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BaseEntityService } from '../../../shared/services/base-entity.service';
import { HttpHelperService } from '../../../shared/services/http-helper.service';
import { AvailabilitySearchCriteriaDTO } from '../models/availability-search-criteria-dto';
import { UserProfileDTO } from '../models/user-profile.dto';

@Injectable()
export class UserProfileService extends BaseEntityService {
  controllerName = 'UserProfile';

  getAllAvailableDrivers(availabilitySearchCriteriaDTO: AvailabilitySearchCriteriaDTO): any {
    return this.httpHelperService.post(this.controllerName + '/GetAllAvailableDrivers', availabilitySearchCriteriaDTO);
  }
  getAllDrivers(availabilitySearchCriteriaDTO: AvailabilitySearchCriteriaDTO): any {
    return this.httpHelperService.post(this.controllerName + '/GetAllDrivers', availabilitySearchCriteriaDTO);
  }

  // postUserProfile(userProfile: UserProfileDTO) {
  //   this.subject.next({ UserProfileDTO: userProfile });
  // }
}