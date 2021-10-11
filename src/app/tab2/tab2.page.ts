import { Component } from '@angular/core';
import { HistoryService } from '../api/history.service';
import { HistoryRecord } from '../models/favourite-pictures.model';
import { PicturesService } from '../api/pictures.service';
import { LoadingController } from '@ionic/angular';


@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {

  loadingDialog: any
  historyArray: HistoryRecord[];
  constructor(private historyService: HistoryService, public loadingController: LoadingController) { }

  ionViewWillEnter() {
    //console.log('Method ionViewWillEnter was called.');      
    this.historyArray = this.historyService.getRecord();
  }

  async presentLoading() {
    this.loadingDialog = await this.loadingController.create(
      {
        message: 'Taking photos ...',
      });
    await this.loadingDialog.present();
  }

  btnRemovePicture(record) {
    console.log("Removing " + record.id);
    this.historyService.removeRecord(record);
    this.ionViewWillEnter();
  }

  public btnRouteToInfo(picture): void {
    console.log("trasfering data....");
    let object = new HistoryRecord(picture)
    //let object = JSON.stringify(picture);
    this.historyService.passPictureDetail(object);
    //this.router.navigate(['tab3', object]);
  }
}
