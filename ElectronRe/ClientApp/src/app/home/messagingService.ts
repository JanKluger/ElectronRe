import {Injectable} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {ElectronService} from 'ngx-electron';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {
  constructor(private _electronService: ElectronService) {
  }

  public send(channel: string, object: any): void {
    this._electronService.ipcRenderer.send(channel, object);
  }

  public listen(channel: string): Observable<any> {
    return new Observable((subscriber) => {
      this._electronService.ipcRenderer.on(channel, (event, arg) => {
        subscriber.next(JSON.parse(arg));
      });
    });
  }
}
