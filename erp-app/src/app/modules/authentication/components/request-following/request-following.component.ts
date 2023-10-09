import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-request-following',
  templateUrl: './request-following.component.html',
  styleUrls: ['./request-following.component.css']
})
export class RequestFollowingComponent {

  requestId: number;
  userId: number;
  constructor(private route: ActivatedRoute,
    private authService: AuthService,
    public translate: TranslateService,

  ) {

  }

  ngOnInit(): void {
    let requestId = this.route.snapshot.paramMap.get('id');
    if (requestId) {
      this.requestId = parseInt(requestId);
    }

    let userId = this.route.snapshot.paramMap.get('userId');
    if (userId) {
      this.userId = parseInt(userId);
    }

  }
}
