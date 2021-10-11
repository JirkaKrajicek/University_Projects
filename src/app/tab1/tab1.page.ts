import { Component } from '@angular/core';
import { PicturesService } from '../api/pictures.service';
import { LoadingController } from '@ionic/angular';
import { HistoryRecord } from '../models/favourite-pictures.model';
import { HistoryService } from '../api/history.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})

export class Tab1Page {

  myinput: String
  outputData: String[]
  loadingDialog: any
  selectedFavouritePicture: any
  /*auxArray: HistoryRecord[] = []
  auxRecord: HistoryRecord*/

  constructor(private picturesService: PicturesService, public loadingController: LoadingController, private historyService: HistoryService, public router: Router) {

  }

  public btnSearchClicked(): void {
    if (this.myinput !== undefined && this.myinput.length > 0) {
      console.log(this.myinput);
      this.presentLoading();

      this.picturesService.getPictures(this.myinput).subscribe((data) => {
        //console.log(data);
        data.photos.forEach(p => {
          p.baseWord = this.myinput;
        });
        console.log(data.photos);
        this.outputData = data.photos;


        this.loadingDialog.dismiss();
        //console.log(data.photos);
      });
    }
    else {
      console.log("input is empty");
    }
  }

  public btnSaveFavouriteClicked(picture): void {
    /*picture.baseWord = this.myinput;
    console.log(picture);*/

    let record = new HistoryRecord(picture);
    //console.log(record);

    this.historyService.saveRecord(record);
  }

  async presentLoading() {
    this.loadingDialog = await this.loadingController.create(
      {
        message: 'Taking photos ...',
      });
    await this.loadingDialog.present();
  }

  public btnRouteToInfo(picture): void {
    console.log("trasfering data....");
    let object = new HistoryRecord(picture);
    this.historyService.passPictureDetail(object);

  }
}
