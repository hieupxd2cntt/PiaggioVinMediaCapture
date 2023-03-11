import 'dart:io';

import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/objectmodel/doctypeguideinsertmodel.dart';
import 'package:VinMediaCapture/objectmodel/users.dart';
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

Future<http.Response> GetLogin(Users user) async {
  var response = await http.post(
    Uri.parse(apiUrl + 'User/Login'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(user),
  );
  return response;
}

Future<http.Response> GetListAttribute(String barcode) async {
  var response = await http.post(
    Uri.parse(apiUrl + 'Mobile/GetListAttribute'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(barcode),
  );
  return response;
}

Future<Response?> PostDocTypeGuideItem(
    MobileDoctypeGuideInsertModel docTypeGuide, String fileName) async {
  List<MobileDoctypeGuideInsertModel> docTypeGuides = [];
  List<MultipartFile> images = [];
  docTypeGuides.add(docTypeGuide);
  var image = await MultipartFile.fromFile(fileName, filename: "dp");
  images.add(image);

  FormData formData = new FormData.fromMap({
    "docTypeGuides": json.encode(docTypeGuides),
    "images": images,
  });
  try {
    Response response = await Dio().post(
      apiUrl + 'Mobile/InsertDocTypeGuide',
      data: formData,
      options: Options(headers: <String, String>{
        'Authorization': 'Bearer',
      }),
    );
    return response;
  } catch (ex) {
    var b = 1;
  }
  return null;
  /*var response = await http.post(
    Uri.parse(apiUrl + 'Mobile/InsertDocTypeGuide'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(docTypeGuide),
  );
  return response;*/
}
