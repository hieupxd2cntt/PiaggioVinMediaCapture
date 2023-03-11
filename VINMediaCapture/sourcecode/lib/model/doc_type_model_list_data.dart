import 'dart:convert';
import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/objectmodel/DocTypeItemAddModel.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';

class DocTypeModelListData {
  DocTypeModelListData({
    this.imagePath = '',
    this.assetImage = '',
    this.titleTxt = '',
    this.subTxt = "",
    this.attrDocType = 0,
    this.textValue = "",
    this.itemId = 0,
    this.attrId = 0,
  });

  String imagePath;
  String titleTxt;
  String subTxt;
  String assetImage;
  String textValue;
  int attrDocType;
  int itemId;
  int attrId;
  static Future<List<DocTypeModelListData>> getAttrDataType() async {
    try {
      var sessionManager = SessionManager();
      var vincode = await sessionManager.get("currentVinCode");
      var getListAttribute = await GetListAttribute(vincode);
      var tagObjsJson = jsonDecode(getListAttribute.body) as List;
      List<DocTypeItemAddModel> attrs = tagObjsJson
          .map((tagJson) => DocTypeItemAddModel.fromJson(tagJson))
          .toList();
      ModelList.clear();
      for (var element in attrs) {
        int attrDocType = 0;
        if (element.docTypeItemAttr != null) {
          attrDocType = element.docTypeItemAttr!.attrDataType;
        }
        var value = DocTypeModelListData(
          attrDocType: element.docTypeItemAttr!.attrDataType,
          imagePath: hostUrl +
              element
                  .docTypeItems!.itemImage, //element.docTypeItems?.itemImage,
          subTxt: element.docTypeItems!.itemDescription,
          titleTxt: element.docTypeItems!.itemName,
          itemId: element.docTypeItems!.itemID,
          attrId: element.docTypeItemAttr!.attrID,
        );
        ModelList.add(value);
      }
    } catch (ee) {}
    return ModelList;
  }

  static List<DocTypeModelListData> ModelList = <DocTypeModelListData>[
    /* DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_1.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_2.png',
      titleTxt: 'Queen Hotel',
      subTxt: 'Wembley, London',
    ),*/
    /*DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_3.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_4.png',
      titleTxt: 'Queen Hotel',
      subTxt: 'Wembley, London',
    ),
    DocTypeModelListData(
      imagePath: 'assets/hotel/hotel_5.png',
      titleTxt: 'Grand Royal Hotel',
      subTxt: 'Wembley, London',
    ),*/
  ];
}
