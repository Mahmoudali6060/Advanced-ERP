import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { LocalStorageItems } from '../constants/local-storage-items';
import { LocalStorageService } from './local-storage.service';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';
import { UserProfileDTO } from 'src/app/modules/user-management/models/user-profile.dto';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  public selectedUser = new Subject<UserProfileDTO>();
  //  this.getSelectedUser()
  
  constructor(private localStorageService: LocalStorageService, private userProfileService: UserProfileService) {

  }
  getSelectedUser():Observable<UserProfileDTO> {
    return this.selectedUser.asObservable();
    //return this.localStorageService.getItem(LocalStorageItems.userProfile) as UserProfileDTO;
  }
  sendUserProfile(userProfile: UserProfileDTO) {
    this.selectedUser.next(userProfile);
  }
}