import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit {
  typeSelected: string;
  @Input() isLoading :boolean = false
	@Output() afterLoaded: EventEmitter<boolean> = new EventEmitter<boolean>();


  constructor(private spinnerService: NgxSpinnerService) { 
    this.typeSelected = 'ball-clip-rotate-multiple';
		// setTimeout(() => {
		// 	this.spinnerService.hide();
		//   }, 1000);
  }

  ngOnInit(): void {
    if(this.isLoading === true){
      this.spinnerService.show();
    }
    else{
			this.spinnerService.hide();
    }
  }

}
