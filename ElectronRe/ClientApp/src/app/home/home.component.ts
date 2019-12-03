import {ChangeDetectorRef, Component} from '@angular/core';
import {MessagingService} from './messagingService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  dataList: DataList[];

  constructor(private msg: MessagingService, private router: Router, private _cdRef: ChangeDetectorRef) {
    this.msg.listen('TestSend').subscribe(
      data => {
        console.log('getting new data', data);
        this.dataList = data.Data;
        this._cdRef.detectChanges();
      }, error => {
        console.log('ERROR', error);
      }
    );
    this.msg.send('TestGet', {});
  }

  routeToCounter() {
    console.log('routing');
    this.router.navigate(['/counter']);
  }

}

export interface DataList {
  Id: string;
  Title: string;
}
