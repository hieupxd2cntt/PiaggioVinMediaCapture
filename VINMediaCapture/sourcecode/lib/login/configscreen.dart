import 'dart:convert';
import 'dart:io';
import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/login/loginscreen.dart';
import 'package:VinMediaCapture/objectmodel/configmodel.dart';
import 'package:global_configuration/global_configuration.dart';
import 'package:path_provider/path_provider.dart';
import 'Component.dart';
import 'package:flutter/material.dart';
import 'Toast.dart';
import 'package:flutter/services.dart' show rootBundle;

class configscreen extends StatefulWidget {
  const configscreen({super.key});

  @override
  State<configscreen> createState() => _configscreenState();
}

class _configscreenState extends State<configscreen> {
  bool loading = false;

  final hostUrlCon = TextEditingController();
  final apiUrlCon = TextEditingController();
  final _formkey = GlobalKey<FormState>();
  String configFilePath = "";
  @override
  void initState() {
    getApplicationDocumentsDirectory().then((value) => ReadAppSetting(value));
  }

  void ReadAppSetting(Directory dir) async {
    if (!(dir.existsSync())) {
      dir.create();
    }
    configFilePath = dir.path + '/appsetting.txt';
    var file = new File(dir.path + '/appsetting.txt');
    if (file.existsSync()) {
      try {
        var cfg = file.readAsStringSync();
        var cfgModel = ConfigModel.fromJson(jsonDecode(cfg));
        hostUrlCon.text = cfgModel.hostUrl;
        apiUrlCon.text = cfgModel.apiUrl;
      } catch (ex) {}
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          Padding(
            padding: const EdgeInsets.all(0.0),
            child: Column(
              // crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  height: 15,
                ),
                Center(),
                SizedBox(
                  height: 20,
                ),
                Padding(
                  padding: const EdgeInsets.all(12),
                  child: Container(
                    width: MediaQuery.of(context).size.height * 0.4,
                    height: MediaQuery.of(context).size.height * 0.7,
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(12),
                      child: Column(
                        children: [
                          SizedBox(
                            height: 15,
                          ),
                          Text(
                            "Cấu hình hệ thống",
                            style: TextStyle(
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                                fontSize: 24),
                          ),
                          SizedBox(
                            height: 45,
                          ),
                          Form(
                            key: _formkey,
                            child: Column(
                              children: [
                                TextFormField(
                                    keyboardType: TextInputType.text,
                                    controller: hostUrlCon,
                                    decoration: InputDecoration(
                                      hintText: "Host Url",
                                    )),
                                TextFormField(
                                  keyboardType: TextInputType.text,
                                  controller: apiUrlCon,
                                  decoration: InputDecoration(
                                    hintText: "API Url",
                                  ),
                                ),
                              ],
                            ),
                          ),
                          SizedBox(
                            height: 5,
                          ),
                          SizedBox(
                            height: 55,
                          ),
                          roundbutton(
                              title: "Lưu",
                              tapfun: () async {
                                var cfgModel = new ConfigModel(
                                    hostUrlCon.text, apiUrlCon.text);
                                var file = new File(configFilePath);
                                file.writeAsStringSync(
                                    jsonEncode(cfgModel.toJson()));

                                toastmessage("Lưu cấu hình thành công");
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) => loginscreen()),
                                );
                              }),
                          SizedBox(
                            height: 10,
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
